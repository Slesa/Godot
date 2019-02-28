export class TodoEntry {
    id: number;
    text: string;
    done: boolean;
    archieved: boolean;

    constructor(text:string) {
        this.id = 0;
        this.text = text;
        this.done = false;
        this.archieved = false;
    }
}