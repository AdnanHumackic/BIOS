import {Observable} from "rxjs";

export interface MyBaseEndpoint<TRequest, TResponse>{
  Akcija(request: TRequest): Observable<TResponse>;
}
