import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { appsettings } from '../Settings/appsettings';
import { Director } from '../Models/Director';
import { ResponseAPI } from '../Models/ResponseAPI';

@Injectable({
  providedIn: 'root'
})
export class DirectorService {

  private http = inject(HttpClient);
  private apiUrl:string = appsettings.apiUrl + "Director";

  constructor() { }

  lista(){
    return this.http.get<Director[]>(this.apiUrl);
  }
  
  obtener(id:number){
    return this.http.get<Director>(`${this.apiUrl}/${id}`);
  }

  crear(objeto:Director){
    return this.http.post<ResponseAPI>(this.apiUrl,objeto);
  }

  editar(objeto:Director){
    return this.http.put<ResponseAPI>(this.apiUrl,objeto);
  }

  eliminar(id:number){
    return this.http.delete<ResponseAPI>(`${this.apiUrl}/${id}`);
  }
}