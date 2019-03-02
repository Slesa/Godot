import { Component, OnInit } from '@angular/core';
import { TodoEntry } from '../todoentry';
import { TodoService } from '../todo.service';
import { MessageService } from '../message.service';

export enum ViewMode { All = 0, Open = 1, Done  = 2 }

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.css']
})
export class TodosComponent implements OnInit {

  todoEntries: TodoEntry[];
  leftEntries: TodoEntry[];
  leftEntryCount: number = 0;
  doneEntries: TodoEntry[];
  doneEntryCount: number = 0;
  viewMode: ViewMode;
  oldText: string;

  constructor(
    private todoService: TodoService,
    private messages: MessageService) { }

  ngOnInit() {
    // this.messages.add(`Todos init`);
    this.viewMode = ViewMode.All;
    this.getTodoEntries();
  }

  getTodoEntries(): void {
    this.todoService.getTodoEntries()
      .subscribe(
        todos => {
          //this.messages.add(`loaded ${todos.length}`);
          this.todoEntries = todos;
          this.buildLists();
        });
  }

  private buildLists(): void {
    this.leftEntries = this.todoEntries.filter(t => t.done!==true && t.archieved!==true);
    this.leftEntryCount = this.leftEntries.length;
    this.doneEntries = this.todoEntries.filter(t => t.done===true && t.archieved!==true);
    this.doneEntryCount = this.doneEntries.length;
  }

  onToggle($event) {
    $event.todo.done = !$event.todo.done;
    this.buildLists();
  }
  onEntryAdded($event) {
    //this.messages.add(`Reloading entries due to ${$event.event} / ${$event.todo}`);
    this.todoEntries.push($event.todo);
    this.buildLists();
  }
  onEntryRemove($event) {
    //this.messages.add(`Reloading entries due to ${$event.event} / ${$event.todo}`);
    this.todoEntries = this.todoEntries.filter(todo => todo.id !== $event.todo.id);
    this.buildLists();
    this.todoService.delTodoEntry($event.todo).subscribe();
  }

  onClearCompleted($event) {
    console.log(`Todos: On clear completed`);
    this.todoEntries = this.todoEntries.filter(todo => todo.done!==true);
    this.buildLists();
  }

  onEntryEdit($event) {
    this.messages.add(`edit ${$event.todo.text}`);
    this.oldText = $event.todo.text;
    $event.todo.editing = true;
  }
  onEditDone($event) {
    this.oldText = "";
    $event.todo.editing = false;
  }
  onEditCancel($event) {
    $event.todo.text = this.oldText;
    this.oldText = "";
    $event.todo.editing = false;
  }
}
