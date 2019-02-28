import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { TodoEntry } from '../todoentry';
import { TodoService } from '../todo.service';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-todo-input',
  templateUrl: './todo-input.component.html',
  styleUrls: ['./todo-input.component.css']
})
export class TodoInputComponent implements OnInit {

  @Input()
  newTodo: string;

  @Output()
  onInput : EventEmitter<any> = new EventEmitter<any>();
  
  constructor(
    private todoService: TodoService,
    private messageService: MessageService) { }

  ngOnInit() {
  }

  onEnter(text: string) {
    this.messageService.add(`Entered ${this.newTodo}`)
    this.newTodo = text;
    this.todoService.addTodoEntry(this.newTodo);
    this.onInput.emit(this.newTodo);
  } 
}
