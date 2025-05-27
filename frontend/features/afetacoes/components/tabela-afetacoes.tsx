"use client"

import type { TabelaAfetacoesDto, AfetacaoMensalDto, AfetacaoPerfilDto, CargaMensalDto } from "../types"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { format } from "date-fns"
import { enUS } from "date-fns/locale"

interface TabelaAfetacoesProps {
  tabelaAfetacoes: TabelaAfetacoesDto
}

export default function TabelaAfetacoes({ tabelaAfetacoes }: TabelaAfetacoesProps) {
  // Verificar se há dados
  if (!tabelaAfetacoes.cargasMensais || tabelaAfetacoes.cargasMensais.length === 0) {
    return <div className="text-center py-12 text-gray-500">No monthly workloads available for this person.</div>
  }

  if (!tabelaAfetacoes.afetacoesPerfis || tabelaAfetacoes.afetacoesPerfis.length === 0) {
    return <div className="text-center py-12 text-gray-500">No profile allocations available for this person.</div>
  }

  // Ordenar cargas mensais por data
  const cargasMensaisOrdenadas = [...tabelaAfetacoes.cargasMensais].sort((a, b) => {
    return new Date(a.mesAno).getTime() - new Date(b.mesAno).getTime()
  })

  // Função para formatar a data
  const formatarData = (dataString: string) => {
    try {
      const data = new Date(dataString)
      return format(data, "MMM/yyyy", { locale: enUS })
    } catch (error) {
      return dataString
    }
  }

  // Função para encontrar a afetação mensal para um perfil e carga mensal específicos
  const encontrarAfetacaoMensal = (
    afetacaoPerfil: AfetacaoPerfilDto,
    cargaMensal: CargaMensalDto,
  ): AfetacaoMensalDto | undefined => {
    if (!tabelaAfetacoes.afetacoesMensais) return undefined

    return tabelaAfetacoes.afetacoesMensais.find(
      (am) => am.afetacaoPerfil.id === afetacaoPerfil.id && am.cargaMensal.id === cargaMensal.id,
    )
  }

  return (
    <div className="overflow-x-auto overflow-y-auto max-h-[70vh] scrollbar-thin scrollbar-thumb-gray-300 scrollbar-track-gray-100">
      <Table className="border-collapse">
        <TableHeader>
          <TableRow>
            <TableHead className="border font-bold bg-gray-100 sticky left-0 z-10">Profile</TableHead>
            <TableHead className="border font-bold bg-gray-100 sticky left-[150px] z-10">Approved PMs</TableHead>
            {cargasMensaisOrdenadas.map((cargaMensal) => (
              <TableHead key={cargaMensal.id} className="border font-bold text-center bg-gray-100 min-w-[100px]">
                {formatarData(cargaMensal.mesAno)}
              </TableHead>
            ))}
          </TableRow>
        </TableHeader>
        <TableBody>
          {tabelaAfetacoes.afetacoesPerfis.map((afetacaoPerfil) => (
            <TableRow key={afetacaoPerfil.id}>
              <TableCell className="border font-medium sticky left-0 bg-white z-10 min-w-[150px]">
                {afetacaoPerfil.perfilDto?.descricao || "Unknown Profile"}
              </TableCell>
              <TableCell className="border text-right sticky left-[150px] bg-white z-10 min-w-[120px]">
                {afetacaoPerfil.pMsAprovados.toFixed(2)}
              </TableCell>
              {cargasMensaisOrdenadas.map((cargaMensal) => {
                const afetacaoMensal = encontrarAfetacaoMensal(afetacaoPerfil, cargaMensal)
                return (
                  <TableCell key={cargaMensal.id} className={`border text-right ${afetacaoMensal ? "bg-blue-50" : ""}`}>
                    {afetacaoMensal ? afetacaoMensal.pMs.toFixed(2) : "-"}
                  </TableCell>
                )
              })}
            </TableRow>
          ))}
          <TableRow className="bg-gray-50 font-bold">
            <TableCell className="border sticky left-0 bg-gray-50 z-10">Total</TableCell>
            <TableCell className="border text-right sticky left-[150px] bg-gray-50 z-10">
              {tabelaAfetacoes.afetacoesPerfis.reduce((total, ap) => total + ap.pMsAprovados, 0).toFixed(2)}
            </TableCell>
            {cargasMensaisOrdenadas.map((cargaMensal) => {
              const totalMensal = tabelaAfetacoes.afetacoesMensais
                ? tabelaAfetacoes.afetacoesMensais
                    .filter((am) => am.cargaMensal.id === cargaMensal.id)
                    .reduce((total, am) => total + am.pMs, 0)
                : 0
              return (
                <TableCell key={cargaMensal.id} className="border text-right font-bold">
                  {totalMensal > 0 ? totalMensal.toFixed(2) : "-"}
                </TableCell>
              )
            })}
          </TableRow>
        </TableBody>
      </Table>
    </div>
  )
}
