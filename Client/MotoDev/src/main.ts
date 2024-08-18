import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/core/components/main/app.component';
import { LoginComponent } from './app/features/auth/components/login/login.component';

bootstrapApplication(AppComponent, appConfig)
  .catch((err) => console.error(err));


