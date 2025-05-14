import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Badge } from "@/components/ui/badge"
import { Button } from "@/components/ui/button"
import { Card, CardContent } from "@/components/ui/card"
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
  const tasks = [
    {
      id: 1,
      title: "Design homepage wireframes",
      description: "Create wireframes for the new homepage design based on client feedback",
      project: "Website Redesign",
      priority: "High",
      status: "In Progress",
      dueDate: "2025-05-20",
      assignee: {
        name: "Alex Morgan",
        avatar: "/placeholder.svg?height=32&width=32",
      },
    },
    {
      id: 2,
      title: "Implement authentication flow",
      description: "Build user authentication system with login, registration and password reset",
      project: "Mobile App Development",
      priority: "Medium",
      status: "Not Started",
      dueDate: "2025-05-22",
      assignee: {
        name: "Jamie Chen",
        avatar: "/placeholder.svg?height=32&width=32",
      },
    },
    {
      id: 3,
      title: "Create social media content calendar",
      description: "Develop content calendar for Q2 marketing campaign across all platforms",
      project: "Marketing Campaign",
      priority: "Low",
      status: "Completed",
      dueDate: "2025-05-25",
      assignee: {
        name: "Taylor Kim",
        avatar: "/placeholder.svg?height=32&width=32",
      },
    },
    {
      id: 4,
      title: "Set up analytics dashboard",
      description: "Configure analytics dashboard to track key performance metrics",
      project: "CRM Integration",
      priority: "Medium",
      status: "In Progress",
      dueDate: "2025-05-18",
      assignee: {
        name: "Jordan Smith",
        avatar: "/placeholder.svg?height=32&width=32",
      },
    },
    {
      id: 5,
      title: "Prepare data schema for migration",
      description: "Design database schema for the new system and plan migration strategy",
      project: "Data Migration",
      priority: "High",
      status: "Not Started",
      dueDate: "2025-05-30",
      assignee: {
        name: "Casey Lee",
        avatar: "/placeholder.svg?height=32&width=32",
      },
    },
    {
      id: 6,
      title: "Optimize product images",
      description: "Compress and optimize all product images for faster loading times",
      project: "E-commerce Platform",
      priority: "Low",
      status: "In Progress",
      dueDate: "2025-05-19",
      assignee: {
        name: "Alex Morgan",
        avatar: "/placeholder.svg?height=32&width=32",
      },
    },
    {
      id: 7,
      title: "Implement payment gateway",
      description: "Integrate Stripe payment processing for online transactions",
      project: "E-commerce Platform",
      priority: "High",
      status: "Not Started",
      dueDate: "2025-05-28",
      assignee: {
        name: "Jamie Chen",
        avatar: "/placeholder.svg?height=32&width=32",
      },
    },
    {
      id: 8,
      title: "Create user onboarding flow",
      description: "Design and implement user onboarding experience for new users",
      project: "Mobile App Development",
      priority: "Medium",
      status: "In Progress",
      dueDate: "2025-05-24",
      assignee: {
        name: "Jordan Smith",
        avatar: "/placeholder.svg?height=32&width=32",
      },
    },
  ]

  const getPriorityColor = (priority: string) => {
    switch (priority) {
      case "High":
        return "bg-red-500/20 text-red-700 hover:bg-red-500/30"
      case "Medium":
        return "bg-amber-500/20 text-amber-700 hover:bg-amber-500/30"
      case "Low":
        return "bg-green-500/20 text-green-700 hover:bg-green-500/30"
      default:
        return "bg-gray-500/20 text-gray-700 hover:bg-gray-500/30"
    }
  }

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
              <DropdownMenuLabel>Priority</DropdownMenuLabel>
              <DropdownMenuCheckboxItem checked>High</DropdownMenuCheckboxItem>
              <DropdownMenuCheckboxItem checked>Medium</DropdownMenuCheckboxItem>
              <DropdownMenuCheckboxItem checked>Low</DropdownMenuCheckboxItem>
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
              <DropdownMenuItem>Priority (High to Low)</DropdownMenuItem>
              <DropdownMenuItem>Priority (Low to High)</DropdownMenuItem>
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
          <Card>
            <CardContent className="p-0">
              <div className="divide-y">
                {tasks.map((task) => (
                  <div key={task.id} className="flex items-start p-4 hover:bg-muted/50">
                    <Checkbox id={`task-${task.id}`} className="mt-1 mr-4" />
                    <div className="flex-1 min-w-0">
                      <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-2">
                        <div>
                          <div className="font-medium">{task.title}</div>
                          <div className="text-sm text-muted-foreground">{task.project}</div>
                        </div>
                        <div className="flex flex-wrap items-center gap-2">
                          <Badge variant="outline" className={getPriorityColor(task.priority)}>
                            {task.priority}
                          </Badge>
                          <Badge variant="outline" className={getStatusColor(task.status)}>
                            {task.status}
                          </Badge>
                          <div className="flex items-center gap-1 text-sm text-muted-foreground">
                            <Calendar className="h-3 w-3" />
                            <span>{new Date(task.dueDate).toLocaleDateString()}</span>
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
                            <DropdownMenuItem>Set Priority</DropdownMenuItem>
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
