import { Component, OnInit } from '@angular/core';
import { TodoEntry } from '../todoentry';
import { TodoService } from '../todo.service';
import { MessageService } from '../message.service';

@Component({
  selector: 'app-todo-input',
  templateUrl: './todo-input.component.html',
  styleUrls: ['./todo-input.component.css']
})
export class TodoInputComponent implements OnInit {

  newTodo: string;
  
  constructor(
    private todoService: TodoService,
    private messageService: MessageService) { }

  ngOnInit() {
  }

  onEnter(text: string) {
    this.messageService.add(`Adding ${text}`)
    this.newTodo = text;
    this.todoService.updateTodoEntry({text} as TodoEntry);
  } 
}
