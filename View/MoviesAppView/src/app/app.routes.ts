import { Routes } from '@angular/router';
import { Dashboard } from './Pages/dashboard/dashboard';
import { DirectorComponent } from './Pages/director/director';
import { MovieComponent } from './Pages/movie/movie';

export const routes: Routes = [
    {path:'',component:Dashboard},
    {path:'dashboard',component:Dashboard},
    {path:'director/:id',component:DirectorComponent},
    {path:'movie/:id',component:MovieComponent}
];
