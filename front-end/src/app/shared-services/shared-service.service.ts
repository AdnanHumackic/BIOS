import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SharedServiceService {

  constructor() { }


  ID:number=0;
  setSearchID(ID:number)
  {
    this.ID = ID;
  }
  getSearchID()
  {
    return this.ID;
  }

}
