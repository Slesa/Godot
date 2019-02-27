import { Component, OnInit } from '@angular/core';
import { TodoEntry } from '../todoentry';
import { TODOENTRIES } from '../mock-todoentries';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.css']
})
export class TodosComponent implements OnInit {

  todoentries = TODOENTRIES;

  constructor() { }

  ngOnInit() {
  }

}
