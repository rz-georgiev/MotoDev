import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule],  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})

export class LoginComponent {

  isLoggedIn: boolean = true;
 
  multipleCats = [
     {id: 1, name: "Kitty bob", imgSrc: "https://cdn.britannica.com/70/234870-050-D4D024BB/Orange-colored-cat-yawns-displaying-teeth.jpg"},
     {id: 2, name: "Kitty meow", imgSrc: "https://www.wfla.com/wp-content/uploads/sites/71/2023/05/GettyImages-1389862392.jpg?w=2560&h=1440&crop=1"},
     {id: 3, name: "Kitty bau", imgSrc: "https://upload.wikimedia.org/wikipedia/commons/thumb/3/3a/Cat03.jpg/1200px-Cat03.jpg"},
     {id: 4, name: "Kitty floppy", imgSrc: "https://www.cats.org.uk/media/13136/220325case013.jpg?width=500&height=333.49609375"},
   ];
 }