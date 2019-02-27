import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-todo-status',
  templateUrl: './todo-status.component.html',
  styleUrls: ['./todo-status.component.css']
})
export class TodoStatusComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    console.log(`Todo Status init`);
  }

}
