import { Component, OnInit } from '@angular/core';
import { TodoEntry } from '../todoentry';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.css']
})
export class TodosComponent implements OnInit {

  todo: TodoEntry = {
    id: 1,
    text: "Needs to be done",
    done: false,
    archieved: false
  };

  constructor() { }

  ngOnInit() {
  }

}
