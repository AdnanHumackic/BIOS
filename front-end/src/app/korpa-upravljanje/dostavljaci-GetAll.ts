export interface DostavljacGetAllResponse{
  dostavljac: Dostavljac[]
}

export interface Dostavljac {
  id: number
  naziv: string
  cijenaDostave: number
  sjediste: string
}
