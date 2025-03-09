import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CombatSceneComponent } from './features/combat-scene/combat-scene.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CombatSceneComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'Frontend';
}
