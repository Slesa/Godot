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

  onToggle(todo) {
    todo.done = todo.done;
    this.buildLists();
  }
  onEntryAdded($event) {
    //this.messages.add(`Reloading entries due to ${$event.event} / ${$event.todo}`);
    this.todoEntries.push($event.todo);
    this.buildLists();
  }
  onEntryRemove(todo) {
    //this.messages.add(`Reloading entries due to ${$event.event} / ${$event.todo}`);
    this.todoEntries = this.todoEntries.filter(t => t.id !== todo.id);
    this.buildLists();
    this.todoService.delTodoEntry(todo).subscribe();
  }

  onClearCompleted($event) {
    console.log(`Todos: On clear completed`);
    this.todoEntries = this.todoEntries.filter(todo => todo.done!==true);
    this.buildLists();
  }

  onEntryEdit(todo) {
    this.messages.add(`edit ${todo.text}`);
    this.oldText = todo.text;
    todo.editing = true;
  }
  onEditDone(todo) {
    this.oldText = "";
    todo.editing = false;
  }
  onEditCancel(todo) {
    todo.text = this.oldText;
    this.oldText = "";
    todo.editing = false;
  }
}
