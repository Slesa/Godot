import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { TodoEntry } from '../todoentry';
import { ViewMode } from '../todos/todos.component';

@Component({
  selector: 'app-todo-status',
  templateUrl: './todo-status.component.html',
  styleUrls: ['./todo-status.component.css']
})
export class TodoStatusComponent implements OnInit {

  @Input()  remainingCount: number = 0;
  @Input()  doneEntryCount: number = 0;
  @Input()  viewMode: ViewMode;

  @Output()
  clearCompleted = new EventEmitter();

  constructor() { }

  ngOnInit() {
    console.log(`Todo Status init`);
  }

  onClearCompleted($event): void {
    console.log(`Status: On clear completed`);
    this.clearCompleted.emit($event);
  }
}
