using DofusCharacterTracker.Database;
using DofusCharacterTracker.Database.Tables;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace DofusCharacterTracker.Pages;

public sealed partial class AddCharacter
{
    [Inject] private IDbContextFactory<Db> DbFactory { get; set; } = null!;
    [Inject] private NavigationManager Navigator { get; set; } = null!;

    public NewCharacter Model { get; } = new();
    
    public async Task DoCreateCharacter()
    {
        await using var db = await DbFactory.CreateDbContextAsync();

        var character = new Character()
        {
            Name = Model.Name,
            Class = Model.Class,
            Level = Model.Level,
            AlmanaxProgress = Model.AlmanaxProgress,
        };

        db.Characters.Add(character);
        
        if(Model.Element1 != null)
        {
            db.CharacterElements.Add(new CharacterElement()
            {
                CharacterId = character.Id,
                Element = Model.Element1.Value
            });
        }
        
        if(Model.Element2 != null)
        {
            db.CharacterElements.Add(new CharacterElement()
            {
                CharacterId = character.Id,
                Element = Model.Element2.Value
            });
        }

        await db.SaveChangesAsync();
        Navigator.NavigateTo("/");
    }
}

public sealed class NewCharacter
{
    public string Name { get; set; } = "";

    public Class Class { get; set; } = Class.Cra;
    
    public int Level { get; set; } = 50;

    public Element? Element1 { get; set; }
    public Element? Element2 { get; set; }

    public AlmanaxProgress AlmanaxProgress { get; set; } = AlmanaxProgress.BarelyStarted;

    public class Validator: AbstractValidator<NewCharacter>
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