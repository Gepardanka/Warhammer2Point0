import { NgStyle } from '@angular/common';
import {
  Component,
  computed,
  effect,
  ElementRef,
  Input,
  input,
  OnDestroy,
  OnInit,
  ViewChild,
  WritableSignal,
} from '@angular/core';
import { CharacterDTO } from '../../models/character-dto.model';

@Component({
  selector: 'app-character',
  imports: [NgStyle],
  templateUrl: './character.component.html',
  styleUrl: './character.component.css',
})
export class CharacterComponent implements OnInit, OnDestroy {
  @ViewChild('animationElement') animationElement!: ElementRef<HTMLDivElement>;
  @Input({ required: true }) position!: {
    left?: string;
    right?: string;
    bottom: string;
    height: string;
    width: string;
    transform: string;
  };
  @Input({ required: true }) damagedCharId!: WritableSignal<string>;
  character = input.required<CharacterDTO>();

  health = computed(() => this.character().health / this.maxHealth);

  private maxHealth = 0;
  private stopEffect;

  constructor() {
    this.stopEffect = effect(() => {
      const triggeredId = this.damagedCharId();
      if (triggeredId === this.character().guid) {
        this.playAnimationAndSFX();
      }
    });
  }

  playAnimationAndSFX(): Promise<void> {
    return new Promise((resolve) => {
      const animEl = this.animationElement.nativeElement;
      animEl.classList.add('animate-damage');
      console.log(
        `${this.character.name}, ${this.character().health}, ${this.maxHealth}`
      );
      setTimeout(() => {
        animEl.classList.remove('animate-damage');
        resolve();
      }, 2000);
    });
  }

  ngOnInit(): void {
    this.maxHealth = this.character().health;
  }

  ngOnDestroy(): void {
    this.stopEffect.destroy();
  }
}
