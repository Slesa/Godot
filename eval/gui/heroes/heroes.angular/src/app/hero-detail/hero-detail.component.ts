import { Component, OnInit, Input } from '@angular/core';
import { Location } from '@angular/common';
import { HeroService } from '../hero.service';
import { Hero } from '../model/hero';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-hero-detail',
  templateUrl: './hero-detail.component.html',
  styleUrls: ['./hero-detail.component.css']
})
export class HeroDetailComponent implements OnInit {

  @Input() hero: Hero;
  
  constructor(
    private _route: ActivatedRoute,
    private _heroService: HeroService,
    private _location: Location
  ) { }

  ngOnInit() {
    this.getHero();
  }

  getHero(): void {
    const id = +this._route.snapshot.paramMap.get('id');
    this._heroService.getHero(id).subscribe(hero => this.hero = hero);
  }

  save(): void {
    this._heroService.updateHero(this.hero).subscribe(() => this.goBack());
  }
  goBack(): void {
    this._location.back();
  }
}
