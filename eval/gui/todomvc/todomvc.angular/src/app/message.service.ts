import { Injectable } from '@angular/core';
import { MessagesComponent } from './messages/messages.component';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  messages: string[] = [];

  constructor() { }

  add(msg: string): void {
    this.messages.push(msg);
  }

  clear(): void {
    this.messages = [];
  } 
}
