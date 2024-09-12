import { Component, EventEmitter, Output, output } from '@angular/core';

@Component({
  selector: 'app-custom-file-uploader',
  standalone: true,
  imports: [],
  templateUrl: './custom-file-uploader.component.html',
  styleUrl: './custom-file-uploader.component.css'
})
export class CustomFileUploaderComponent {

  @Output() onCustomFileSelected = new EventEmitter<FormData>();

  public onFileSelected(event: Event): FormData {
    const input = event?.target as HTMLInputElement;
    const formData = new FormData();
    if (input.files && input.files.length > 0) {

      const file = input.files[0] as File;
      formData.append('file', file, file.name)
    }
    this.onCustomFileSelected.emit(formData);
    return formData;
  }
}
