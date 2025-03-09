import { Component, inject } from '@angular/core';
import { RoundService } from '../../services/round.service';

@Component({
  selector: 'app-combat-scene',
  imports: [],
  templateUrl: './combat-scene.component.html',
  styleUrl: './combat-scene.component.css',
})
export class CombatSceneComponent {
  roundService = inject(RoundService);

  constructor() {
    this.roundService.startGame([]);
  }
}
