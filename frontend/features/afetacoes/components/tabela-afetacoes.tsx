"use client"

import { useState } from "react"
import type { TabelaAfetacoesDto, AfetacaoMensalDto, AfetacaoPerfilDto, CargaMensalDto } from "../types"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { format } from "date-fns"
import { enUS } from "date-fns/locale"
import { Switch } from "@/components/ui/switch"
import { Label } from "@/components/ui/label"
import EditAfetacaoDialog from "./edit-afetacao-dialog"

interface TabelaAfetacoesProps {
  tabelaAfetacoes: TabelaAfetacoesDto
  onAfetacaoUpdated: () => void
}

export default function TabelaAfetacoes({ tabelaAfetacoes, onAfetacaoUpdated }: TabelaAfetacoesProps) {
  const [showHours, setShowHours] = useState(false)
  const [editDialogOpen, setEditDialogOpen] = useState(false)
  const [selectedAfetacaoPerfil, setSelectedAfetacaoPerfil] = useState<AfetacaoPerfilDto | null>(null)
  const [selectedCargaMensal, setSelectedCargaMensal] = useState<CargaMensalDto | null>(null)
  const [selectedAfetacaoMensal, setSelectedAfetacaoMensal] = useState<AfetacaoMensalDto | null>(null)

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

  // Função para calcular horas potenciais trabalháveis
  const calcularHorasPotenciais = (cargaMensal: CargaMensalDto): number => {
    return cargaMensal.jornadaDiaria * cargaMensal.diasUteisTrabalhaveis
  }

  // Função para converter PMs em horas
  const pmParaHoras = (pm: number, cargaMensal: CargaMensalDto): number => {
    return pm * calcularHorasPotenciais(cargaMensal)
  }

  // Função para abrir o diálogo de edição
  const handleOpenEditDialog = (
    afetacaoPerfil: AfetacaoPerfilDto,
    cargaMensal: CargaMensalDto,
    afetacaoMensal?: AfetacaoMensalDto,
  ) => {
    setSelectedAfetacaoPerfil(afetacaoPerfil)
    setSelectedCargaMensal(cargaMensal)
    setSelectedAfetacaoMensal(afetacaoMensal || null)
    setEditDialogOpen(true)
  }

  // Função para fechar o diálogo de edição
  const handleCloseEditDialog = () => {
    setEditDialogOpen(false)
    setSelectedAfetacaoPerfil(null)
    setSelectedCargaMensal(null)
    setSelectedAfetacaoMensal(null)
  }

  return (
    <div>
      <div className="flex items-center justify-end space-x-2 mb-4">
        <Label htmlFor="show-hours" className="text-sm font-medium">
          {showHours ? "Show Hours" : "Show PMs"}
        </Label>
        <Switch id="show-hours" checked={showHours} onCheckedChange={setShowHours} />
      </div>

      <div className="overflow-x-auto overflow-y-auto max-h-[70vh] scrollbar-thin scrollbar-thumb-gray-300 scrollbar-track-gray-100">
        <Table className="border-collapse">
          <TableHeader>
            <TableRow>
              <TableHead className="border font-bold bg-gray-100 sticky left-0 z-10">Profile</TableHead>
              <TableHead className="border font-bold bg-gray-100 sticky left-[150px] z-10">
                {showHours ? "Approved Hours" : "Approved PMs"}
              </TableHead>
              <TableHead className="border font-bold bg-gray-100 sticky left-[270px] z-10">
                {showHours ? "Total Hours Used" : "Total PMs Used"}
              </TableHead>
              {cargasMensaisOrdenadas.map((cargaMensal) => (
                <TableHead key={cargaMensal.id} className="border font-bold text-center bg-gray-100 min-w-[100px]">
                  {formatarData(cargaMensal.mesAno)}
                </TableHead>
              ))}
            </TableRow>
          </TableHeader>
          <TableBody>
            {/* Linha: Daily Workday */}
            <TableRow className="bg-blue-50 text-sm">
              <TableCell className="border font-medium sticky left-0 bg-blue-50 z-10">Daily Workday</TableCell>
              <TableCell className="border sticky left-[150px] bg-blue-50 z-10"></TableCell>
              <TableCell className="border sticky left-[270px] bg-blue-50 z-10"></TableCell>
              {cargasMensaisOrdenadas.map((cargaMensal) => (
                <TableCell key={`daily-${cargaMensal.id}`} className="border text-center bg-blue-50">
                  {cargaMensal.jornadaDiaria.toFixed(1)}h
                </TableCell>
              ))}
            </TableRow>

            {/* Linha: Working Days */}
            <TableRow className="bg-blue-50 text-sm">
              <TableCell className="border font-medium sticky left-0 bg-blue-50 z-10">Working Days</TableCell>
              <TableCell className="border sticky left-[150px] bg-blue-50 z-10"></TableCell>
              <TableCell className="border sticky left-[270px] bg-blue-50 z-10"></TableCell>
              {cargasMensaisOrdenadas.map((cargaMensal) => (
                <TableCell key={`days-${cargaMensal.id}`} className="border text-center bg-blue-50">
                  {cargaMensal.diasUteisTrabalhaveis.toFixed(0)} days
                </TableCell>
              ))}
            </TableRow>

            {/* Linha: Potential Working Hours */}
            <TableRow className="bg-blue-50 text-sm">
              <TableCell className="border font-medium sticky left-0 bg-blue-50 z-10">
                Potential Working Hours
              </TableCell>
              <TableCell className="border sticky left-[150px] bg-blue-50 z-10"></TableCell>
              <TableCell className="border sticky left-[270px] bg-blue-50 z-10"></TableCell>
              {cargasMensaisOrdenadas.map((cargaMensal) => (
                <TableCell key={`potential-${cargaMensal.id}`} className="border text-center bg-blue-50">
                  {calcularHorasPotenciais(cargaMensal).toFixed(1)}h
                </TableCell>
              ))}
            </TableRow>

            {/* Linha: Vacation Days */}
            <TableRow className="bg-blue-50 text-sm">
              <TableCell className="border font-medium sticky left-0 bg-blue-50 z-10">Vacation Days</TableCell>
              <TableCell className="border sticky left-[150px] bg-blue-50 z-10"></TableCell>
              <TableCell className="border sticky left-[270px] bg-blue-50 z-10"></TableCell>
              {cargasMensaisOrdenadas.map((cargaMensal) => (
                <TableCell key={`vacation-days-${cargaMensal.id}`} className="border text-center bg-blue-50">
                  {cargaMensal.feriasBaixasLicencasFaltas.toFixed(0)} days
                </TableCell>
              ))}
            </TableRow>

            {/* Linha: Base Salary */}
            <TableRow className="bg-blue-50 text-sm">
              <TableCell className="border font-medium sticky left-0 bg-blue-50 z-10">Base Salary</TableCell>
              <TableCell className="border sticky left-[150px] bg-blue-50 z-10"></TableCell>
              <TableCell className="border sticky left-[270px] bg-blue-50 z-10"></TableCell>
              {cargasMensaisOrdenadas.map((cargaMensal) => (
                <TableCell key={`salary-${cargaMensal.id}`} className="border text-center bg-blue-50">
                  €{cargaMensal.salarioBase.toFixed(2)}
                </TableCell>
              ))}
            </TableRow>

            {/* Linha: Vacation Hours */}
            <TableRow className="bg-blue-50 text-sm">
              <TableCell className="border font-medium sticky left-0 bg-blue-50 z-10">Vacation Hours</TableCell>
              <TableCell className="border sticky left-[150px] bg-blue-50 z-10"></TableCell>
              <TableCell className="border sticky left-[270px] bg-blue-50 z-10"></TableCell>
              {cargasMensaisOrdenadas.map((cargaMensal) => (
                <TableCell key={`vacation-hours-${cargaMensal.id}`} className="border text-center bg-blue-50">
                  {(cargaMensal.feriasBaixasLicencasFaltas * cargaMensal.jornadaDiaria).toFixed(1)}h
                </TableCell>
              ))}
            </TableRow>

            {/* Linhas de afetações de perfil */}
            {tabelaAfetacoes.afetacoesPerfis.map((afetacaoPerfil) => (
              <TableRow key={afetacaoPerfil.id}>
                <TableCell className="border font-medium sticky left-0 bg-white z-10 min-w-[150px]">
                  {afetacaoPerfil.perfilDto?.descricao || "Unknown Profile"}
                </TableCell>
                <TableCell className="border text-center sticky left-[150px] bg-white z-10 min-w-[120px]">
                  {showHours
                    ? `${(afetacaoPerfil.pMsAprovados * 160).toFixed(1)}h` // Assumindo 160h como padrão para PMs aprovados
                    : afetacaoPerfil.pMsAprovados.toFixed(2)}
                </TableCell>
                <TableCell className="border text-center sticky left-[270px] bg-white z-10 min-w-[120px]">
                  {(() => {
                    // Calculate total PMs used for this profile across all months
                    const totalPMsUsed = tabelaAfetacoes.afetacoesMensais
                      ? tabelaAfetacoes.afetacoesMensais
                          .filter((am) => am.afetacaoPerfil.id === afetacaoPerfil.id)
                          .reduce((total, am) => total + am.pMs, 0)
                      : 0

                    return showHours
                      ? `${(totalPMsUsed * 160).toFixed(1)}h` // Assuming 160h as standard for total PMs used
                      : totalPMsUsed.toFixed(2)
                  })()}
                </TableCell>
                {cargasMensaisOrdenadas.map((cargaMensal) => {
                  const afetacaoMensal = encontrarAfetacaoMensal(afetacaoPerfil, cargaMensal)
                  return (
                    <TableCell
                      key={cargaMensal.id}
                      className={`border text-center ${
                        afetacaoMensal ? "bg-blue-50" : ""
                      } hover:bg-blue-100 cursor-pointer`}
                      onClick={() => handleOpenEditDialog(afetacaoPerfil, cargaMensal, afetacaoMensal)}
                    >
                      {afetacaoMensal
                        ? showHours
                          ? `${pmParaHoras(afetacaoMensal.pMs, cargaMensal).toFixed(1)}h`
                          : afetacaoMensal.pMs.toFixed(2)
                        : "-"}
                    </TableCell>
                  )
                })}
              </TableRow>
            ))}

            {/* Linha de totais */}
            <TableRow className="bg-gray-50 font-bold">
              <TableCell className="border sticky left-0 bg-gray-50 z-10">Total</TableCell>
              <TableCell className="border text-center sticky left-[150px] bg-gray-50 z-10">
                {showHours
                  ? `${(
                      tabelaAfetacoes.afetacoesPerfis.reduce((total, ap) => total + ap.pMsAprovados, 0) * 160
                    ).toFixed(1)}h`
                  : tabelaAfetacoes.afetacoesPerfis.reduce((total, ap) => total + ap.pMsAprovados, 0).toFixed(2)}
              </TableCell>
              <TableCell className="border text-center sticky left-[270px] bg-gray-50 z-10">
                {(() => {
                  const grandTotal = tabelaAfetacoes.afetacoesMensais
                    ? tabelaAfetacoes.afetacoesMensais.reduce((total, am) => total + am.pMs, 0)
                    : 0

                  return showHours ? `${(grandTotal * 160).toFixed(1)}h` : grandTotal.toFixed(2)
                })()}
              </TableCell>
              {cargasMensaisOrdenadas.map((cargaMensal) => {
                const totalMensal = tabelaAfetacoes.afetacoesMensais
                  ? tabelaAfetacoes.afetacoesMensais
                      .filter((am) => am.cargaMensal.id === cargaMensal.id)
                      .reduce((total, am) => total + am.pMs, 0)
                  : 0
                return (
                  <TableCell key={cargaMensal.id} className="border text-center font-bold">
                    {totalMensal > 0
                      ? showHours
                        ? `${pmParaHoras(totalMensal, cargaMensal).toFixed(1)}h`
                        : totalMensal.toFixed(2)
                      : "-"}
                  </TableCell>
                )
              })}
            </TableRow>

            {/* Linha: Work + Absences */}
            <TableRow className="bg-yellow-50 text-sm font-medium">
              <TableCell className="border font-medium sticky left-0 bg-yellow-50 z-10">Work + Absences</TableCell>
              <TableCell className="border sticky left-[150px] bg-yellow-50 z-10"></TableCell>
              <TableCell className="border sticky left-[270px] bg-yellow-50 z-10"></TableCell>
              {cargasMensaisOrdenadas.map((cargaMensal) => {
                // Calculate total work hours for this month
                const totalWorkPMs = tabelaAfetacoes.afetacoesMensais
                  ? tabelaAfetacoes.afetacoesMensais
                      .filter((am) => am.cargaMensal.id === cargaMensal.id)
                      .reduce((total, am) => total + am.pMs, 0)
                  : 0

                // Calculate vacation PMs (vacation days / working days)
                const vacationPMs =
                  cargaMensal.diasUteisTrabalhaveis > 0
                    ? cargaMensal.feriasBaixasLicencasFaltas / cargaMensal.diasUteisTrabalhaveis
                    : 0

                const totalPMs = totalWorkPMs + vacationPMs

                return (
                  <TableCell key={`work-absences-${cargaMensal.id}`} className="border text-center bg-yellow-50">
                    {showHours ? `${pmParaHoras(totalPMs, cargaMensal).toFixed(1)}h` : totalPMs.toFixed(2)}
                  </TableCell>
                )
              })}
            </TableRow>

            {/* Linha: TSU */}
            <TableRow className="bg-orange-50 text-sm font-medium">
              <TableCell className="border font-medium sticky left-0 bg-orange-50 z-10">TSU (%)</TableCell>
              <TableCell className="border sticky left-[150px] bg-orange-50 z-10"></TableCell>
              <TableCell className="border sticky left-[270px] bg-orange-50 z-10"></TableCell>
              {cargasMensaisOrdenadas.map((cargaMensal) => (
                <TableCell key={`tsu-${cargaMensal.id}`} className="border text-center bg-orange-50">
                  {cargaMensal.taxaSocialUnica.toFixed(2)}%
                </TableCell>
              ))}
            </TableRow>

            {/* Linha: FTE Cost */}
            <TableRow className="bg-green-50 text-sm font-medium">
              <TableCell className="border font-medium sticky left-0 bg-green-50 z-10">FTE Cost</TableCell>
              <TableCell className="border sticky left-[150px] bg-green-50 z-10"></TableCell>
              <TableCell className="border sticky left-[270px] bg-green-50 z-10"></TableCell>
              {cargasMensaisOrdenadas.map((cargaMensal) => {
                const fteCost = cargaMensal.salarioBase * (1 + cargaMensal.taxaSocialUnica / 100)
                return (
                  <TableCell key={`fte-cost-${cargaMensal.id}`} className="border text-center bg-green-50">
                    €{fteCost.toFixed(2)}
                  </TableCell>
                )
              })}
            </TableRow>
          </TableBody>
        </Table>
      </div>

      {/* Diálogo de edição */}
      {editDialogOpen && selectedAfetacaoPerfil && selectedCargaMensal && (
        <EditAfetacaoDialog
          isOpen={editDialogOpen}
          onClose={handleCloseEditDialog}
          afetacaoPerfil={selectedAfetacaoPerfil}
          cargaMensal={selectedCargaMensal}
          afetacaoMensal={selectedAfetacaoMensal || undefined}
          onSuccess={onAfetacaoUpdated}
          showHours={showHours}
        />
      )}
    </div>
  )
}
