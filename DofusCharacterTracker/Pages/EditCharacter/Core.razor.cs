using DofusCharacterTracker.Database;
using DofusCharacterTracker.Database.Tables;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace DofusCharacterTracker.Pages.EditCharacter;

public sealed partial class Core
{
    [Inject] private IDbContextFactory<Db> DbFactory { get; set; } = null!;
    [Inject] private NavigationManager Navigator { get; set; } = null!;
    
    [Parameter, EditorRequired] public required Guid CharacterId { get; set; }
    
    public CharacterCoreModel? Model { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        await using var db = await DbFactory.CreateDbContextAsync();
        
        Model = await db.Characters
            .Where(c => c.Id == CharacterId)
            .Select(c => new CharacterCoreModel()
            {
                Name = c.Name,
                Elements = c.Elements!.Select(e => e.Element).ToList(),
                Class = c.Class,
                Level = c.Level
            })
            .FirstOrDefaultAsync();

        if(Model == null)
            Navigator.NavigateTo("/");
    }

    public void AddElement() => Model?.Elements.Add(Element.Strength);
    public void RemoveElement(int index) => Model?.Elements.RemoveAt(index);

    public async Task DoSaveChanges()
    {
        if(Model == null)
            return;
        
        await using var db = await DbFactory.CreateDbContextAsync();

        var character = await db.Characters
            .Include(c => c.Elements)
            .FirstOrDefaultAsync(c => c.Id == CharacterId);

        if(character == null)
        {
            Navigator.NavigateTo("/");
            return;
        }

        character.Name = Model.Name;
        character.Class = Model.Class;
        character.Level = Model.Level;

        foreach(var e in character.Elements!)
            db.Remove(e);
        
        db.CharacterElements.AddRange(Model.Elements
            .Select(e => new CharacterElement()
            {
                Element = e,
                CharacterId = CharacterId
            })
        );

        await db.SaveChangesAsync();
    
        Navigator.NavigateTo("/");
    }
}

public sealed class CharacterCoreModel
{
    public required string Name { get; set; }
    public required List<Element> Elements { get; set; }
    public required Class Class { get; set; }
    public required int Level { get; set; }

    public class Validator: AbstractValidator<CharacterCoreModel>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(x => x.Level)
                .InclusiveBetween(1, 200);
        }
    }
}