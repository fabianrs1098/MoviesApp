import { Component, inject, Input, OnInit } from '@angular/core';

import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {FormBuilder,FormGroup,ReactiveFormsModule} from '@angular/forms';
import { MovieService } from '../../Services/movie';
import { Router } from '@angular/router';
import { Movie } from '../../Models/Movie';

@Component({
  selector: 'app-movie',
  standalone: true,
  imports: [MatFormFieldModule,MatInputModule,MatButtonModule,ReactiveFormsModule],
  templateUrl: './movie.html',
  styleUrl: './movie.css'
})
export class MovieComponent implements OnInit{
  @Input('id') idMovie! : number;
  private movieServicio = inject(MovieService);
  public formBuild = inject(FormBuilder);

  public formMovie:FormGroup = this.formBuild.group({
    name: [''],
    releaseYear:[''],
    gender:[''],
    duration:[''],
    fkDirector:['']
  });

  constructor(private router:Router){}

  ngOnInit(): void {
    if(this.idMovie != 0){
      this.movieServicio.obtener(this.idMovie).subscribe({
        next:(data) =>{
          this.formMovie.patchValue({
            name: data.name,
            releaseYear: data.releaseYear,
            gender: data.gender,
            duration: data.duration,
            fkDirector: data.fkDirector
          })
        },
        error:(err) =>{
          console.log(err.message)
        }
      })
    }
  }

  save(){
    const objeto : Movie = {
      id : this.idMovie,
      name: this.formMovie.value.name,
      releaseYear: this.formMovie.value.releaseYear,
      gender: this.formMovie.value.gender,
      duration: this.formMovie.value.duration,
      fkDirector: this.formMovie.value.fkDirector
    }

    if(this.idMovie == 0){
      this.movieServicio.crear(objeto).subscribe({
        next:(data) =>{
          if(data.isSuccess){
            this.router.navigate(["/"]);
          }else{
            alert("Error al crear")
          }
        },
        error:(err) =>{
          console.log(err.message)
        }
      })
    }else{
      this.movieServicio.editar(objeto).subscribe({
        next:(data) =>{
          if(data.isSuccess){
            this.router.navigate(["/"]);
          }else{
            alert("Error al editar")
          }
        },
        error:(err) =>{
          console.log(err.message)
        }
      })
    }

  }

  return(){
    this.router.navigate(["/"]);
  }
}
