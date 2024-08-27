import { Component, ElementRef, Input, ViewChild } from '@angular/core';

@Component({
  selector: 'app-custom-modal',
  standalone: true,
  imports: [],
  templateUrl: './custom-modal.component.html',
  styleUrl: './custom-modal.component.css'
})
export class CustomModalComponent {
  @ViewChild('modalButton', { static: false }) modalButton!: ElementRef;
  @Input() title!: string; 
  @Input() message!: string; 

  constructor() {
    
  }

  ngAfterViewInit() {
    this.modalButton.nativeElement.click();
  }

}
