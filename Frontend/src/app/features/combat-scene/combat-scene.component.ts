import { Component, HostListener, inject, signal } from '@angular/core';
import { GameService } from '../../services/game.service';
import { NgFor, NgStyle } from '@angular/common';

@Component({
  selector: 'app-combat-scene',
  imports: [NgFor, NgStyle],
  templateUrl: './combat-scene.component.html',
  styleUrl: './combat-scene.component.css',
})
export class CombatSceneComponent {
  roundService = inject(GameService);

  imageCount = 5;
  baseHeight = 400;

  maxVerticalShift = signal(0);
  maxHorizontalShift = signal(0);

  getImagePosition(
    index: number,
    isRightSide: boolean
  ): { left?: string; right?: string; bottom: string; height: string } {
    const k = 0.25;
    const reverseIndex = this.imageCount - index - 1;
    let scale = 1 - 1 / (1 + k * reverseIndex);
    const bottom = `${(this.maxVerticalShift() * scale) / 1.6}px`;
    const xPos = `${this.maxHorizontalShift() * scale * 1.2}px`;
    const height = `${this.baseHeight * (1 - scale)}px`;

    return isRightSide
      ? { right: xPos, bottom, height }
      : { left: xPos, bottom, height };
  }

  ngOnInit(): void {
    this.updateResponsiveValues();
  }

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.updateResponsiveValues();
  }

  updateResponsiveValues() {
    this.maxVerticalShift.set(window.innerHeight - 50);
    this.maxHorizontalShift.set(window.innerWidth / 2);
  }
}
