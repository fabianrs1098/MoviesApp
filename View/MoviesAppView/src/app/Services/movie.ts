import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { appsettings } from '../Settings/appsettings';
import { Movie } from '../Models/Movie';
import { ResponseAPI } from '../Models/ResponseAPI';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  private http = inject(HttpClient);
  private apiUrl:string = appsettings.apiUrl + "Movie";

  constructor() { }

  lista(){
    return this.http.get<Movie[]>(this.apiUrl);
  }
  
  obtener(id:number){
    return this.http.get<Movie>(`${this.apiUrl}/${id}`);
  }

  crear(objeto:Movie){
    return this.http.post<ResponseAPI>(this.apiUrl,objeto);
  }

  editar(objeto:Movie){
    return this.http.put<ResponseAPI>(this.apiUrl,objeto);
  }

  eliminar(id:number){
    return this.http.delete<ResponseAPI>(`${this.apiUrl}/${id}`);
  }
}