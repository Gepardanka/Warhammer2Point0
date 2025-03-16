import { Injectable } from '@angular/core';
import { AudioTrack } from '../models/audio-track.enum';

@Injectable({
  providedIn: 'root',
})
export class AudioServiceService {
  private readonly musicPath = 'assets/music';
  private tracks: { [key: string]: HTMLAudioElement } = {};

  constructor() {
    this.tracks[AudioTrack.MenuBackground] = new Audio(`${this.musicPath}/`);
    this.tracks[AudioTrack.StartBattleSFX] = new Audio(`${this.musicPath}/`);
    this.tracks[AudioTrack.BattleBackground] = new Audio(`${this.musicPath}/`);
    this.tracks[AudioTrack.AttackSFX] = new Audio(`${this.musicPath}/`);
    this.tracks[AudioTrack.HitSFX] = new Audio(`${this.musicPath}/`);
    this.tracks[AudioTrack.DeathSFX] = new Audio(`${this.musicPath}/`);
    this.tracks[AudioTrack.BlockSFX] = new Audio(`${this.musicPath}/`);
    this.tracks[AudioTrack.VictorySFX] = new Audio(`${this.musicPath}/`);

    this.tracks[AudioTrack.MenuBackground].loop = true;
    this.tracks[AudioTrack.BattleBackground].loop = true;
  }

  play(track: AudioTrack) {
    this.stopAll();
    if (this.tracks[track]) {
      this.tracks[track].play();
    }
  }

  stopAll() {
    Object.values(this.tracks).forEach((audio) => audio.pause());
  }

  playSFX(track: AudioTrack) {
    if (this.tracks[track]) {
      this.tracks[track].currentTime = 0;
      this.tracks[track].play();
    }
  }
}
