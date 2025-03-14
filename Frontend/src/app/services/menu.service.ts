import { Injectable, signal, WritableSignal } from '@angular/core';
import { CharacterDTO } from '../models/character-dto.model';
import { CharacterTeam } from '../models/character-team.model';

@Injectable({
  providedIn: 'root',
})
export class MenuService {
  private selectedCharactersLeft = signal<CharacterDTO[]>([]);
  private selectedCharactersRight = signal<CharacterDTO[]>([]);

  getSelectedCharacters(team: CharacterTeam) {
    switch (team) {
      case CharacterTeam.TeamA:
        return this.selectedCharactersLeft.asReadonly();
      case CharacterTeam.TeamB:
        return this.selectedCharactersRight.asReadonly();
      default:
        console.error('Wrong team selected');
        return this.selectedCharactersLeft.asReadonly();
    }
  }

  addCharacter(char: CharacterDTO, team: CharacterTeam) {
    switch (team) {
      case CharacterTeam.TeamA:
        this.addCharacterToCollection(this.selectedCharactersLeft, char);
        break;
      case CharacterTeam.TeamB:
        this.addCharacterToCollection(this.selectedCharactersRight, char);
        break;
    }
  }

  removeCharacter(index: number, team: CharacterTeam) {
    switch (team) {
      case CharacterTeam.TeamA:
        this.removeCharacterToCollection(this.selectedCharactersLeft, index);
        break;
      case CharacterTeam.TeamB:
        this.removeCharacterToCollection(this.selectedCharactersRight, index);
        break;
    }
  }

  private addCharacterToCollection(
    collection: WritableSignal<CharacterDTO[]>,
    char: CharacterDTO
  ) {
    collection.update((array) => [...array, char]);
  }

  private removeCharacterToCollection(
    collection: WritableSignal<CharacterDTO[]>,
    index: number
  ) {
    collection.update((array) => {
      const newArray = [...array];
      newArray.splice(index, 1);
      return newArray;
    });
  }
}
