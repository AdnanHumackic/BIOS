export interface WishlistPaged {
  wishlist: WishlistPagedResponse
}

export interface WishlistPagedResponse {
  dataItems: DataItemWishlist[]
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
  hasPrevios: boolean
  hasNext: boolean
}

export interface DataItemWishlist {
  artikalId: number
  imeArtikla: string
  proizvodjac: string
  cijena: number
  opis: string
  datumDodavanja: string
}
