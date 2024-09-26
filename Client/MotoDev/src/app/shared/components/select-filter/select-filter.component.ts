import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-select-filter',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './select-filter.component.html',
  styleUrl: './select-filter.component.css'
})
export class SelectFilterComponent {
  @Input() items: any[] = [];
  @Input() labelKey: string = 'label';
  @Input() valueKey: string = 'value';
  filterText: string = '';
  filteredItems: any[] = [];
  selectedValue: any;

  ngOnInit(): void {
    this.filteredItems = this.items;
  }

  ngOnChanges(): void {
    this.filterItems();
  }

  filterItems(): void {
    this.filteredItems = this.items.filter(item =>
      item[this.labelKey].toLowerCase().includes(this.filterText.toLowerCase())
    );
  }
}
