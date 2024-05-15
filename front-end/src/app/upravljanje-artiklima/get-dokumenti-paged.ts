export interface DokumentiPaged {
  dataItems: DokumentiPagedResponse[]
  currentPage: number
  totalPages: number
  pageSize: number
  totalCount: number
  hasPrevios: boolean
  hasNext: boolean
}

export interface DokumentiPagedResponse {
  fileUrl: string
  naziv: string
}
