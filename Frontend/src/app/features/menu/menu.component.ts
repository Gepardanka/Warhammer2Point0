import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { GameService } from '../../services/game.service';
import { NgFor, NgIf } from '@angular/common';
import { CharacterDTO } from '../../models/character-dto.model';
import { TeamListComponent } from './team-list/team-list.component';
import { CharacterTeam } from '../../models/character-team.model';
import { MenuService } from '../../services/menu.service';

@Component({
  selector: 'app-menu',
  imports: [TeamListComponent, NgIf],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css',
})
export class MenuComponent implements OnInit {
  private readonly gameService = inject(GameService);
  private readonly menuService = inject(MenuService);

  private selectedCharacters = this.menuService.getSelectedCharacters(
    CharacterTeam.Both
  );

  CharacterTeam = CharacterTeam;
  isGameLoading = signal(false);

  constructor() {
    effect(() => {
      if (this.gameService.gameStarted()) {
        this.isGameLoading.set(false);
      }
    });
  }

  startBattle() {
    this.gameService.fetchBattleHistory(this.selectedCharacters());
    this.isGameLoading.set(true);
  }

  ngOnInit(): void {
    this.gameService.fetchDefaultCharacters();
  }
}
