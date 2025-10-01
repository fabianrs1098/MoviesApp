import { Component, inject } from '@angular/core';

import {MatCardModule} from '@angular/material/card';
import {MatTableModule} from '@angular/material/table';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import { DirectorService } from '../../Services/director';
import { Director } from '../../Models/Director';
import { MovieService } from '../../Services/movie';
import { Movie } from '../../Models/Movie';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [MatCardModule,MatTableModule,MatIconModule,MatButtonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {
  private directorServicio = inject(DirectorService);
  public listaDirectores:Director[] = [];
  public displayedColumnsDirector : string[] = ['name','nationality','age','active','accion'];

  getDirectores(){
    this.directorServicio.lista().subscribe({
      next:(data)=>{
        if(data.length > 0){
          this.listaDirectores = data;
        }
      },
      error:(err)=>{
        console.log(err.message)
      }
    })
  }

  private movieServicio = inject(MovieService);
  public listaMovies:Movie[] = [];
  public displayedColumnsMovie : string[] = ['name','releaseYear','gender','duration','fkDirector','accion'];

  getMovies(){
    this.movieServicio.lista().subscribe({
      next:(data)=>{
        if(data.length > 0){
          this.listaMovies = data;
        }
      },
      error:(err)=>{
        console.log(err.message)
      }
    })
  }

  constructor(private router:Router){
    this.getDirectores();
    this.getMovies();
  }

  createDirector(){
    this.router.navigate(['/director',0]);
  }

  updateDirector(objeto:Director){
    this.router.navigate(['/director',objeto.id]);
  }
  deleteDirector(objeto:Director){
    if(confirm("Desea eliminar el director" + objeto.name)){
      this.directorServicio.eliminar(objeto.id).subscribe({
        next:(data)=>{
          if(data.isSuccess){
            this.getDirectores();
          }else{
            alert("No se pudo eliminar.")
          }
        },
        error:(err)=>{
          console.log(err.message)
        }
      })
    }
  }

  createMovie(){
    this.router.navigate(['/movie',0]);
  }

  updateMovie(objeto:Movie){
    this.router.navigate(['/movie',objeto.id]);
  }
  deleteMovie(objeto:Movie){
    if(confirm("Desea eliminar la pelicula" + objeto.name)){
      this.movieServicio.eliminar(objeto.id).subscribe({
        next:(data)=>{
          if(data.isSuccess){
            this.getMovies();
          }else{
            alert("No se pudo eliminar.")
          }
        },
        error:(err)=>{
          console.log(err.message)
        }
      })
    }
  }
}
