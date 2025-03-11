import { Component, inject, OnInit } from '@angular/core';
import { GameService } from '../../services/game.service';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-menu',
  imports: [NgFor],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css',
})
export class MenuComponent implements OnInit {
  gameService = inject(GameService);

  defaultCharacters = this.gameService.defaultCharacters;

  ngOnInit(): void {
    this.gameService.fetchDefaultCharacters();
  }
}
