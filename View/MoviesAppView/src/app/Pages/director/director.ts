import { Component, Input, OnInit, inject } from '@angular/core';

import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {FormBuilder,FormGroup,ReactiveFormsModule} from '@angular/forms';
import { DirectorService } from '../../Services/director';
import { Router } from '@angular/router';
import { Director } from '../../Models/Director';

@Component({
  selector: 'app-director',
  standalone: true,
  imports: [MatFormFieldModule,MatInputModule,MatButtonModule,ReactiveFormsModule],
  templateUrl: './director.html',
  styleUrl: './director.css'
})
export class DirectorComponent implements OnInit {
  @Input('id') idDirector! : number;
  private directorServicio = inject(DirectorService);
  public formBuild = inject(FormBuilder);

  public formDirector:FormGroup = this.formBuild.group({
    name: [''],
    nationality:[''],
    age:[0],
    active:['true']
  });

  constructor(private router:Router){}

  ngOnInit(): void {
    if(this.idDirector != 0){
      this.directorServicio.obtener(this.idDirector).subscribe({
        next:(data) =>{
          this.formDirector.patchValue({
            name: data.name,
            nationality: data.nationality,
            age: data.age,
            active: data.active
          })
        },
        error:(err) =>{
          console.log(err.message)
        }
      })
    }
  }

  save(){
    const objeto : Director = {
      id : this.idDirector,
      name: this.formDirector.value.name,
      nationality: this.formDirector.value.nationality,
      age:this.formDirector.value.age,
      active:this.formDirector.value.active,
    }

    if(this.idDirector == 0){
      this.directorServicio.crear(objeto).subscribe({
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
      this.directorServicio.editar(objeto).subscribe({
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
