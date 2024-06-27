import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './components/app/app.config';
import { AppComponent } from './components/app/app.component';
import { LoginComponent } from './components/login/login.component';

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));


