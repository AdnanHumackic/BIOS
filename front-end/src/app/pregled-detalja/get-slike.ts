/*export type GetSlike = Slike[]

export interface Slike {
  fileContents: string
  contentType: string
  fileDownloadName: string
  lastModified: any
  entityTag: any
  enableRangeProcessing: boolean
}
*/

export type Root = SlikeResponse[]

export interface SlikeResponse {
  slika: string
  redniBroj: number

}
