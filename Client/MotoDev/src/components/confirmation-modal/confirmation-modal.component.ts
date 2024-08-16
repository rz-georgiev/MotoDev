import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-confirmation-modal',
  standalone: true,
  imports: [],
  templateUrl: './confirmation-modal.component.html',
  styleUrl: './confirmation-modal.component.css'
})
export class ConfirmationModalComponent {
  
  @Input() questionMessage: string | undefined;
  @Output() handleConfirmation = new EventEmitter<boolean>();

  constructor() {
    
  }

  cancel() {
    this.handleConfirmation.emit(false);
  }

  confirm() {
    this.handleConfirmation.emit(true);
  }

}
