import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter',
  standalone: false
})
export class FilterPipe implements PipeTransform {

  transform(items: any[], searchText: string): any[] {
    if (!items) return [];
    if (!searchText) return items;

    searchText = searchText.toLowerCase();

    return items.filter(item => this.searchObject(item, searchText));
  }

  private searchObject(item: any, searchText: string): boolean {
    if (!item) return false;
    
    if (typeof item !== 'object') {
      return String(item).toLowerCase().includes(searchText);
    }
    
    return Object.keys(item).some(key => {
      const value = item[key];
      
      if (value === null || value === undefined) {
        return false;
      }
      
      if (typeof value === 'object') {
        return this.searchObject(value, searchText);
      }
      
      return String(value).toLowerCase().includes(searchText);
    });
  }
  

}
