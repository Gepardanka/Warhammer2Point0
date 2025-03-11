import { Component } from '@angular/core';
import { MenuComponent } from './features/menu/menu.component';
import { CombatSceneComponent } from './features/combat-scene/combat-scene.component';

@Component({
  selector: 'app-root',
  imports: [MenuComponent, CombatSceneComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {}
