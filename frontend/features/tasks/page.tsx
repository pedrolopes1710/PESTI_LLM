"use client"

import { useEffect, useState } from "react"
import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Badge } from "@/components/ui/badge"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Checkbox } from "@/components/ui/checkbox"
import {
  DropdownMenu,
  DropdownMenuCheckboxItem,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu"
import { Input } from "@/components/ui/input"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { ArrowUpDown, Calendar, ChevronDown, Filter, MoreHorizontal, Search } from "lucide-react"
import type { Activity } from "./types"
import { fetchTasksGroupedByActivity } from "./api"
import { Skeleton } from "@/components/ui/skeleton"
import { Alert, AlertDescription, AlertTitle } from "@/components/ui/alert"
import { AlertCircle, RefreshCw } from "lucide-react"

import { Toaster } from "@/components/ui/toaster"
import { Progress } from "@/components/ui/progress"

export default function TasksPage() {
  const [activities, setActivities] = useState<Activity[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const [searchTerm, setSearchTerm] = useState("")

  // Função para carregar os dados da API
  const fetchData = async () => {
    try {
      setLoading(true)
      setError(null)

      // Buscar tarefas agrupadas por atividade usando a função da API
      const groupedActivities = await fetchTasksGroupedByActivity()
      console.log("Atividades agrupadas:", groupedActivities)

      setActivities(groupedActivities)
    } catch (error) {
      console.error("Error fetching data:", error)
      setError("Não foi possível carregar as tarefas. Verifique se a API está em execução.")
    } finally {
      setLoading(false)
    }
  }

  // Carregar dados quando o componente montar
  useEffect(() => {
    fetchData()
  }, [])

  // Filtrar atividades e tarefas com base no termo de pesquisa
  const filteredActivities = activities
    .map((activity) => ({
      ...activity,
      tasks: activity.tasks.filter(
        (task) =>
          task.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
          task.description.toLowerCase().includes(searchTerm.toLowerCase()) ||
          activity.name.toLowerCase().includes(searchTerm.toLowerCase()),
      ),
    }))
    .filter((activity) => activity.tasks.length > 0)

  const getStatusColor = (status: string) => {
    switch (status) {
      case "In Progress":
        return "bg-blue-500/20 text-blue-700 hover:bg-blue-500/30"
      case "Not Started":
        return "bg-slate-500/20 text-slate-700 hover:bg-slate-500/30"
      case "Completed":
        return "bg-green-500/20 text-green-700 hover:bg-green-500/30"
      default:
        return "bg-gray-500/20 text-gray-700 hover:bg-gray-500/30"
    }
  }

  // Componente de carregamento
  const LoadingSkeleton = () => (
    <>
      {[1, 2, 3].map((i) => (
        <Card key={i} className="mb-6">
          <CardHeader className="bg-muted/50 py-3">
            <Skeleton className="h-6 w-48" />
          </CardHeader>
          <CardContent className="p-4">
            {[1, 2].map((j) => (
              <div key={j} className="flex items-start gap-4 py-4 border-b last:border-0">
                <Skeleton className="h-4 w-4 rounded-sm mt-1" />
                <div className="flex-1 space-y-2">
                  <Skeleton className="h-5 w-full max-w-md" />
                  <Skeleton className="h-4 w-32" />
                  <Skeleton className="h-4 w-full" />
                  <div className="flex justify-between items-center pt-2">
                    <Skeleton className="h-8 w-8 rounded-full" />
                    <Skeleton className="h-8 w-8 rounded-full" />
                  </div>
                </div>
              </div>
            ))}
          </CardContent>
        </Card>
      ))}
    </>
  )

  // Função para lidar com a criação de uma nova tarefa
  const handleTaskCreated = () => {
    // Recarregar os dados após a criação de uma nova tarefa
    fetchData()
  }

  return (
    <div className="space-y-6">
      <Toaster />
      <div className="flex flex-col gap-2">
        <h1 className="text-3xl font-bold tracking-tight">Tasks</h1>
        <p className="text-muted-foreground">Manage and track all your tasks</p>
      </div>

      <div className="flex flex-col sm:flex-row items-start sm:items-center justify-between gap-4">
        <div className="flex items-center w-full max-w-sm gap-2">
          <Search className="h-4 w-4 text-muted-foreground absolute ml-3 pointer-events-none" />
          <Input
            placeholder="Search tasks..."
            className="pl-9"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </div>
        <div className="flex items-center gap-2">
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="outline">
                <Filter className="h-4 w-4 mr-2" />
                Filter
                <ChevronDown className="h-4 w-4 ml-2" />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end" className="w-56">
              <DropdownMenuLabel>Filter By</DropdownMenuLabel>
              <DropdownMenuSeparator />
              <DropdownMenuLabel>Status</DropdownMenuLabel>
              <DropdownMenuCheckboxItem checked>In Progress</DropdownMenuCheckboxItem>
              <DropdownMenuCheckboxItem>Not Started</DropdownMenuCheckboxItem>
              <DropdownMenuCheckboxItem>Completed</DropdownMenuCheckboxItem>
              <DropdownMenuSeparator />
              <DropdownMenuLabel>Activity</DropdownMenuLabel>
              {activities.map((activity) => (
                <DropdownMenuCheckboxItem key={activity.id} checked>
                  {activity.name}
                </DropdownMenuCheckboxItem>
              ))}
            </DropdownMenuContent>
          </DropdownMenu>
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="outline">
                <ArrowUpDown className="h-4 w-4 mr-2" />
                Sort
                <ChevronDown className="h-4 w-4 ml-2" />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuItem>Due Date (Ascending)</DropdownMenuItem>
              <DropdownMenuItem>Due Date (Descending)</DropdownMenuItem>
              <DropdownMenuItem>Activity</DropdownMenuItem>
              <DropdownMenuItem>Project</DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
      </div>

      {error && (
        <Alert variant="destructive">
          <AlertCircle className="h-4 w-4" />
          <AlertTitle>Erro</AlertTitle>
          <AlertDescription className="flex justify-between items-center">
            {error}
            <Button variant="outline" size="sm" onClick={fetchData} className="ml-2">
              <RefreshCw className="h-4 w-4 mr-2" />
              Tentar novamente
            </Button>
          </AlertDescription>
        </Alert>
      )}

      <Tabs defaultValue="all" className="space-y-4">
        <TabsList>
          <TabsTrigger value="all">All Tasks</TabsTrigger>
          <TabsTrigger value="my">My Tasks</TabsTrigger>
          <TabsTrigger value="upcoming">Upcoming</TabsTrigger>
          <TabsTrigger value="completed">Completed</TabsTrigger>
        </TabsList>
        <TabsContent value="all" className="space-y-4">
          {loading ? (
            <LoadingSkeleton />
          ) : filteredActivities.length > 0 ? (
            filteredActivities.map((activity) => (
              <Card key={activity.id} className="mb-6">
                <CardHeader className="bg-muted/50 py-3">
                  <CardTitle className="text-lg flex flex-col md:flex-row md:items-center justify-between gap-2">
                    <div>
                      <span>{activity.name}</span>
                      <p className="text-sm text-muted-foreground mt-1">{activity.description}</p>
                    </div>
                    <div className="flex flex-col md:items-end gap-2">
                      <div className="flex items-center gap-3">
                        <div className="flex flex-col md:items-end text-sm">
                          <div className="flex items-center gap-1">
                            <Calendar className="h-3 w-3 text-muted-foreground" />
                            <span className="text-muted-foreground">
                              {new Date(activity.dataInicio).toLocaleDateString()} -{" "}
                              {new Date(activity.dataFim).toLocaleDateString()}
                            </span>
                          </div>
                        </div>
                        <Badge variant="outline" className="ml-2">
                          {activity.tasks.length} tasks
                        </Badge>
                      </div>

                      {/* Barra de progresso */}
                      <div className="w-full md:w-48 flex flex-col gap-1">
                        {(() => {
                          const completedTasks = activity.tasks.filter((task) => task.status === "Completed").length
                          const progressPercentage =
                            activity.tasks.length > 0 ? Math.round((completedTasks / activity.tasks.length) * 100) : 0

                          return (
                            <>
                              <div className="flex items-center justify-between text-xs">
                                <span>Progresso</span>
                                <span>{progressPercentage}%</span>
                              </div>
                              <Progress value={progressPercentage} className="h-2" />
                              <div className="text-xs text-muted-foreground text-right">
                                {completedTasks}/{activity.tasks.length} concluídas
                              </div>
                            </>
                          )
                        })()}
                      </div>
                    </div>
                  </CardTitle>
                </CardHeader>
                <CardContent className="p-0">
                  <div className="divide-y">
                    {activity.tasks.map((task) => (
                      <div key={task.id} className="flex items-start p-4 hover:bg-muted/50">
                        <Checkbox id={`task-${task.id}`} className="mt-1 mr-4" />
                        <div className="flex-1 min-w-0">
                          <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-2">
                            <div>
                              <div className="font-medium">{task.title}</div>
                              <div className="text-sm text-muted-foreground">{task.project}</div>
                            </div>
                            <div className="flex flex-wrap items-center gap-2">
                              <Badge variant="outline" className={getStatusColor(task.status)}>
                                {task.status}
                              </Badge>
                            </div>
                          </div>
                          <p className="text-sm text-muted-foreground mt-2">{task.description}</p>
                          <div className="flex items-center justify-between mt-4">
                            <Avatar className="h-8 w-8">
                              <AvatarImage src={task.assignee.avatar || "/placeholder.svg"} alt={task.assignee.name} />
                              <AvatarFallback>{task.assignee.name.charAt(0)}</AvatarFallback>
                            </Avatar>
                            <DropdownMenu>
                              <DropdownMenuTrigger asChild>
                                <Button variant="ghost" size="icon">
                                  <MoreHorizontal className="h-4 w-4" />
                                  <span className="sr-only">Actions</span>
                                </Button>
                              </DropdownMenuTrigger>
                              <DropdownMenuContent align="end">
                                <DropdownMenuItem>Edit Task</DropdownMenuItem>
                                <DropdownMenuItem>Change Assignee</DropdownMenuItem>
                                <DropdownMenuItem>Change Activity</DropdownMenuItem>
                                <DropdownMenuItem>Mark as Complete</DropdownMenuItem>
                              </DropdownMenuContent>
                            </DropdownMenu>
                          </div>
                        </div>
                      </div>
                    ))}
                  </div>
                </CardContent>
              </Card>
            ))
          ) : (
            <Card>
              <CardContent className="p-6 text-center text-muted-foreground">
                {searchTerm ? "Nenhuma tarefa encontrada para a pesquisa." : "Nenhuma tarefa disponível."}
              </CardContent>
            </Card>
          )}
        </TabsContent>
        <TabsContent value="my" className="space-y-4">
          <Card>
            <CardContent className="p-6 text-center text-muted-foreground">
              Filtered view of tasks assigned to you would appear here.
            </CardContent>
          </Card>
        </TabsContent>
        <TabsContent value="upcoming" className="space-y-4">
          <Card>
            <CardContent className="p-6 text-center text-muted-foreground">
              Filtered view of upcoming tasks would appear here.
            </CardContent>
          </Card>
        </TabsContent>
        <TabsContent value="completed" className="space-y-4">
          <Card>
            <CardContent className="p-6 text-center text-muted-foreground">
              Filtered view of completed tasks would appear here.
            </CardContent>
          </Card>
        </TabsContent>
      </Tabs>
    </div>
  )
}
