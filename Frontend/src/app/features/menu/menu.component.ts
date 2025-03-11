import { Component, inject, OnInit, signal } from '@angular/core';
import { GameService } from '../../services/game.service';
import { NgFor } from '@angular/common';
import { CharacterDTO } from '../../models/character-dto.model';
import { TeamListComponent } from "./team-list/team-list.component";
import { CharacterTeam } from '../../models/character-team.model';

@Component({
  selector: 'app-menu',
  imports: [TeamListComponent],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css',
})
export class MenuComponent implements OnInit {
  private readonly gameService = inject(GameService);

  CharacterTeam = CharacterTeam;
  isGameLoading = false;

  startGame(){
    //this.gameService.fetchBattleHistory();
    this.isGameLoading = true;
  }

  ngOnInit(): void {
    this.gameService.fetchDefaultCharacters();
  }
}
