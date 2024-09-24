import {Pipe, PipeTransform} from '@angular/core';
import * as moment from 'moment-timezone';

@Pipe({
  standalone: true,
  name: 'utcToLocal',
})
export class UtcToLocalPipe implements PipeTransform {
  transform(value: string): string {
    if (!value) return '';

    const timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;
    return moment.utc(`${value}Z`).tz(timeZone).format('DD.MM.yyyy HH:mm');
  }
}