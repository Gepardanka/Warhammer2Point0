import { Component, HostListener, inject, Input, signal } from '@angular/core';
import { GameService } from '../../services/game.service';
import { NgFor, NgStyle } from '@angular/common';
import { RoundHistory } from '../../models/round-history.model';

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

  @Input() fightHistory!: RoundHistory

  // getImagePosition(
  //   index: number,
  //   isRightSide: boolean
  // ): { left?: string; right?: string; bottom: string; height: string } {
  //   const k = 0.25;
  //   const reverseIndex = this.imageCount - index - 1;
  //   let scale = 1 - 1 / (1 + k * reverseIndex);
  //   const bottom = `${(this.maxVerticalShift() * scale) / 1.6}px`;
  //   const xPos = `${this.maxHorizontalShift() * scale * 1.2}px`;
  //   const height = `${this.baseHeight * (1 - scale)}px`;

  //   return isRightSide
  //     ? { right: xPos, bottom, height }
  //     : { left: xPos, bottom, height };
  // }


  ngOnInit(): void {

    //na potrzeby testowania do usunięcia jak wsyzstko bedzie działać
    console.log(this.fightHistory)
    for(const character of this.fightHistory.teamA){
      if(character.name == "Elf"){character.bigURL = "/assets/images/characters/elf.png"}
      if(character.name == "Człowiek"){character.bigURL = "/assets/images/characters/warrior.png"} 
      if(character.name == "Krasnolud"){character.bigURL = "/assets/images/characters/dwarf.png"}
      if(character.name == "Niziołek"){character.bigURL = "/assets/images/characters/niziolek.png"}     
    }
    for(const character of this.fightHistory.teamB){
      if(character.name == "Elf"){character.bigURL = "/assets/images/characters/elf.png"}
      if(character.name == "Człowiek"){character.bigURL = "/assets/images/characters/warrior.png"} 
      if(character.name == "Krasnolud"){character.bigURL = "/assets/images/characters/dwarf.png"}
      if(character.name == "Niziołek"){character.bigURL = "/assets/images/characters/niziolek.png"}     
    }

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
