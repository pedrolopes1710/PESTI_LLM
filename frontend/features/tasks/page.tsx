'use client'
import { useState, useEffect } from 'react' // Importando hooks do React
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
import { ArrowUpDown, Calendar, ChevronDown, Filter, MoreHorizontal, Plus, Search } from "lucide-react"


 
 
export default function TasksPage() {
  const [posts, setPosts] = useState(null)
 
  useEffect(() => {
    async function fetchPosts() {
      try {
        const res = await fetch('https://localhost:7284/api/Tarefas')
        if(!res.ok) {
          throw new Error('Network response was not ok')
        }
        const data = await res.json()
        console.log(data);
        setPosts(data)
      } catch (error) {
        console.error("Error fetching posts:", error)
      }
    }
    fetchPosts()
  }, [])

  // Modelo de dados com atividades e tarefas associadas
  const activities = [
    {
      id: 1,
      name: "Desenvolvimento Frontend",
      description: "Atividades relacionadas ao desenvolvimento da interface do usuário",
      tasks: [
        {
          id: 101,
          title: "Design homepage wireframes",
          description: "Create wireframes for the new homepage design based on client feedback",
          project: "Website Redesign",
          status: "In Progress",
          dataInicio: "2025-05-20",
          dataFim: "2025-05-25",
          assignee: {
            name: "Alex Morgan",
            avatar: "/placeholder.svg?height=32&width=32",
          },
        },
        {
          id: 102,
          title: "Implementar componentes React",
          description: "Desenvolver componentes reutilizáveis para o sistema",
          project: "Website Redesign",
          status: "Not Started",
          dataInicio: "2025-05-26",
          dataFim: "2025-06-02",
          assignee: {
            name: "Jamie Chen",
            avatar: "/placeholder.svg?height=32&width=32",
          },
        },
        {
          id: 103,
          title: "Otimizar performance",
          description: "Melhorar o tempo de carregamento e a responsividade da aplicação",
          project: "Website Redesign",
          status: "Not Started",
          dataInicio: "2025-06-03",
          dataFim: "2025-06-10",
          assignee: {
            name: "Riley Johnson",
            avatar: "/placeholder.svg?height=32&width=32",
          },
        },
      ],
    },
    {
      id: 2,
      name: "Desenvolvimento Backend",
      description: "Atividades relacionadas à implementação da lógica de negócios e APIs",
      tasks: [
        {
          id: 201,
          title: "Implementar autenticação",
          description: "Desenvolver sistema de login e registro de usuários",
          project: "CRM Integration",
          status: "In Progress",
          dataInicio: "2025-05-15",
          dataFim: "2025-05-22",
          assignee: {
            name: "Morgan Taylor",
            avatar: "/placeholder.svg?height=32&width=32",
          },
        },
        {
          id: 202,
          title: "Criar endpoints de API",
          description: "Desenvolver endpoints RESTful para comunicação com o frontend",
          project: "CRM Integration",
          status: "Not Started",
          dataInicio: "2025-05-23",
          dataFim: "2025-05-30",
          assignee: {
            name: "Casey Lee",
            avatar: "/placeholder.svg?height=32&width=32",
          },
        },
      ],
    },
    {
      id: 3,
      name: "Design UX/UI",
      description: "Atividades relacionadas ao design da experiência do usuário",
      tasks: [
        {
          id: 301,
          title: "Criar guia de estilos",
          description: "Desenvolver guia de estilos com cores, tipografia e componentes",
          project: "Mobile App Development",
          status: "Completed",
          dataInicio: "2025-05-10",
          dataFim: "2025-05-15",
          assignee: {
            name: "Jordan Smith",
            avatar: "/placeholder.svg?height=32&width=32",
          },
        },
        {
          id: 302,
          title: "Prototipar telas principais",
          description: "Criar protótipos interativos das principais telas do aplicativo",
          project: "Mobile App Development",
          status: "In Progress",
          dataInicio: "2025-05-16",
          dataFim: "2025-05-23",
          assignee: {
            name: "Jordan Smith",
            avatar: "/placeholder.svg?height=32&width=32",
          },
        },
      ],
    },
  ]

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

  return (
    <div className="space-y-6">
      <div className="flex flex-col gap-2">
        <h1 className="text-3xl font-bold tracking-tight">Tasks</h1>
        <p className="text-muted-foreground">Manage and track all your tasks</p>
      </div>
      <div className="flex flex-col sm:flex-row items-start sm:items-center justify-between gap-4">
        <div className="flex items-center w-full max-w-sm gap-2">
          <Search className="h-4 w-4 text-muted-foreground absolute ml-3 pointer-events-none" />
          <Input placeholder="Search tasks..." className="pl-9" />
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
          <Button>
            <Plus className="h-4 w-4 mr-2" />
            Add Task
          </Button>
        </div>
      </div>

      <Tabs defaultValue="all" className="space-y-4">
        <TabsList>
          <TabsTrigger value="all">All Tasks</TabsTrigger>
          <TabsTrigger value="my">My Tasks</TabsTrigger>
          <TabsTrigger value="upcoming">Upcoming</TabsTrigger>
          <TabsTrigger value="completed">Completed</TabsTrigger>
        </TabsList>
        <TabsContent value="all" className="space-y-4">
          {activities.map((activity) => (
            <Card key={activity.id} className="mb-6">
              <CardHeader className="bg-muted/50 py-3">
                <CardTitle className="text-lg flex items-center justify-between">
                  <span>{activity.name}</span>
                  <Badge variant="outline" className="ml-2">
                    {activity.tasks.length} tasks
                  </Badge>
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
                            <div className="flex items-center gap-1 text-sm text-muted-foreground">
                              <Calendar className="h-3 w-3" />
                              <span>{new Date(task.dataInicio).toLocaleDateString()}</span>
                            </div>
                            <div className="flex items-center gap-1 text-sm text-muted-foreground">
                              <Calendar className="h-3 w-3" />
                              <span>{new Date(task.dataFim).toLocaleDateString()}</span>
                            </div>
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
          ))}
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
