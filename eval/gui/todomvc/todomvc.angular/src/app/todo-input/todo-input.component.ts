import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { TodoEntry } from '../todoentry';
import { TodoService } from '../todo.service';
import { MessageService } from '../message.service';

// https://stackoverflow.com/questions/35390765/pass-parameters-with-eventemitter
@Component({
  selector: 'app-todo-input',
  templateUrl: './todo-input.component.html',
  styleUrls: ['./todo-input.component.css']
})
export class TodoInputComponent implements OnInit {

  @Input()
  newTodo: string;

  @Output()
  entryAdded /*: EventEmitter<TodoEntry>*/ = new EventEmitter/*<TodoEntry>*/();
  
  constructor(
    private todoService: TodoService,
    private messageService: MessageService) { }

  ngOnInit() {
  }

  onEnter(event) {
    this.messageService.add(`Entered ${this.newTodo}`)
    // this.newTodo = text;
    this.todoService.addTodoEntry( new TodoEntry(this.newTodo) )
      .subscribe( todo => 
        {
          this.messageService.add(`Accepted ${todo.text}`)
          this.entryAdded.emit({event, todo});
          this.newTodo = "";
        });
        
  }
}
