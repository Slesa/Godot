import { Component, OnInit } from '@angular/core';
import { TodoEntry } from '../todoentry';
import { TodoService } from '../todo.service';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.css']
})
export class TodosComponent implements OnInit {

  todoEntries: TodoEntry[];
  oldText: string;

  constructor(
    private todoService: TodoService,
    private messages: MessageService) { }

  ngOnInit() {
    this.messages.add(`Todos init`);
    this.getTodoEntries();
  }

  getTodoEntries(): void {
    this.todoService.getTodoEntries()
      .subscribe(
        todos => {
          //this.messages.add(`loaded ${todos.length}`);
          this.todoEntries = todos;
        });
  }

  onEntryAdded($event) {
    //this.messages.add(`Reloading entries due to ${$event.event} / ${$event.todo}`);
    this.todoEntries.push($event.todo);
    // this.getTodoEntries();
  }
  onEntryRemove($event) {
    //this.messages.add(`Reloading entries due to ${$event.event} / ${$event.todo}`);
    this.todoEntries = this.todoEntries.filter(todo => todo.id !== $event.todo.id);
    this.todoService.delTodoEntry($event.todo).subscribe();
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
