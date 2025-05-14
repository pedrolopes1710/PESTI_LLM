import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Badge } from "@/components/ui/badge"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuTrigger } from "@/components/ui/dropdown-menu"
import { Mail, MoreHorizontal, Phone, Plus, Search, UserPlus } from "lucide-react"

export default function TeamPage() {
  const teamMembers = [
    {
      id: 1,
      name: "Alex Morgan",
      email: "alex@example.com",
      phone: "+1 (555) 123-4567",
      role: "Project Manager",
      department: "Management",
      avatar: "/placeholder.svg?height=128&width=128",
      activeProjects: 3,
      activeTasks: 8,
    },
    {
      id: 2,
      name: "Jamie Chen",
      email: "jamie@example.com",
      phone: "+1 (555) 234-5678",
      role: "Senior Developer",
      department: "Engineering",
      avatar: "/placeholder.svg?height=128&width=128",
      activeProjects: 2,
      activeTasks: 6,
    },
    {
      id: 3,
      name: "Taylor Kim",
      email: "taylor@example.com",
      phone: "+1 (555) 345-6789",
      role: "Marketing Specialist",
      department: "Marketing",
      avatar: "/placeholder.svg?height=128&width=128",
      activeProjects: 1,
      activeTasks: 4,
    },
    {
      id: 4,
      name: "Jordan Smith",
      email: "jordan@example.com",
      phone: "+1 (555) 456-7890",
      role: "UX Designer",
      department: "Design",
      avatar: "/placeholder.svg?height=128&width=128",
      activeProjects: 2,
      activeTasks: 5,
    },
    {
      id: 5,
      name: "Casey Lee",
      email: "casey@example.com",
      phone: "+1 (555) 567-8901",
      role: "Data Analyst",
      department: "Analytics",
      avatar: "/placeholder.svg?height=128&width=128",
      activeProjects: 1,
      activeTasks: 3,
    },
    {
      id: 6,
      name: "Morgan Taylor",
      email: "morgan@example.com",
      phone: "+1 (555) 678-9012",
      role: "Backend Developer",
      department: "Engineering",
      avatar: "/placeholder.svg?height=128&width=128",
      activeProjects: 3,
      activeTasks: 7,
    },
    {
      id: 7,
      name: "Riley Johnson",
      email: "riley@example.com",
      phone: "+1 (555) 789-0123",
      role: "Frontend Developer",
      department: "Engineering",
      avatar: "/placeholder.svg?height=128&width=128",
      activeProjects: 2,
      activeTasks: 5,
    },
    {
      id: 8,
      name: "Quinn Davis",
      email: "quinn@example.com",
      phone: "+1 (555) 890-1234",
      role: "Content Strategist",
      department: "Marketing",
      avatar: "/placeholder.svg?height=128&width=128",
      activeProjects: 1,
      activeTasks: 4,
    },
  ]

  return (
    <div className="space-y-6">
      <div className="flex flex-col gap-2">
        <h1 className="text-3xl font-bold tracking-tight">Team</h1>
        <p className="text-muted-foreground">Manage your team members and their access</p>
      </div>

      <div className="flex flex-col sm:flex-row items-start sm:items-center justify-between gap-4">
        <div className="flex items-center w-full max-w-sm gap-2">
          <Search className="h-4 w-4 text-muted-foreground absolute ml-3 pointer-events-none" />
          <Input placeholder="Search team members..." className="pl-9" />
        </div>
        <div className="flex items-center gap-2">
          <Button variant="outline">
            <UserPlus className="h-4 w-4 mr-2" />
            Invite
          </Button>
          <Button>
            <Plus className="h-4 w-4 mr-2" />
            Add Member
          </Button>
        </div>
      </div>

      <Tabs defaultValue="grid" className="space-y-4">
        <TabsList>
          <TabsTrigger value="grid">Grid View</TabsTrigger>
          <TabsTrigger value="list">List View</TabsTrigger>
          <TabsTrigger value="departments">Departments</TabsTrigger>
        </TabsList>
        <TabsContent value="grid" className="space-y-4">
          <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
            {teamMembers.map((member) => (
              <Card key={member.id}>
                <CardHeader className="text-center pb-2">
                  <div className="flex justify-end">
                    <DropdownMenu>
                      <DropdownMenuTrigger asChild>
                        <Button variant="ghost" size="icon">
                          <MoreHorizontal className="h-4 w-4" />
                          <span className="sr-only">Actions</span>
                        </Button>
                      </DropdownMenuTrigger>
                      <DropdownMenuContent align="end">
                        <DropdownMenuItem>View Profile</DropdownMenuItem>
                        <DropdownMenuItem>Edit Member</DropdownMenuItem>
                        <DropdownMenuItem>Assign to Project</DropdownMenuItem>
                        <DropdownMenuItem>Remove</DropdownMenuItem>
                      </DropdownMenuContent>
                    </DropdownMenu>
                  </div>
                  <div className="flex flex-col items-center">
                    <Avatar className="h-20 w-20 mb-2">
                      <AvatarImage src={member.avatar || "/placeholder.svg"} alt={member.name} />
                      <AvatarFallback>{member.name.charAt(0)}</AvatarFallback>
                    </Avatar>
                    <CardTitle>{member.name}</CardTitle>
                    <CardDescription className="mt-1">{member.role}</CardDescription>
                  </div>
                </CardHeader>
                <CardContent className="space-y-4">
                  <div className="flex flex-col gap-2 text-sm">
                    <div className="flex items-center gap-2">
                      <Mail className="h-4 w-4 text-muted-foreground" />
                      <span>{member.email}</span>
                    </div>
                    <div className="flex items-center gap-2">
                      <Phone className="h-4 w-4 text-muted-foreground" />
                      <span>{member.phone}</span>
                    </div>
                  </div>
                  <div className="flex items-center justify-between text-sm">
                    <Badge variant="outline">{member.department}</Badge>
                    <div className="text-muted-foreground">{member.activeProjects} projects</div>
                  </div>
                </CardContent>
              </Card>
            ))}
          </div>
        </TabsContent>
        <TabsContent value="list" className="space-y-4">
          <Card>
            <CardContent className="p-0">
              <div className="divide-y">
                {teamMembers.map((member) => (
                  <div key={member.id} className="flex items-center gap-4 p-4 hover:bg-muted/50">
                    <Avatar className="h-10 w-10">
                      <AvatarImage src={member.avatar || "/placeholder.svg"} alt={member.name} />
                      <AvatarFallback>{member.name.charAt(0)}</AvatarFallback>
                    </Avatar>
                    <div className="flex-1 min-w-0 grid grid-cols-1 md:grid-cols-3 gap-2 md:gap-4">
                      <div>
                        <div className="font-medium">{member.name}</div>
                        <div className="text-sm text-muted-foreground">{member.role}</div>
                      </div>
                      <div className="text-sm">
                        <div className="flex items-center gap-2">
                          <Mail className="h-3 w-3 text-muted-foreground" />
                          <span>{member.email}</span>
                        </div>
                        <div className="flex items-center gap-2 mt-1">
                          <Phone className="h-3 w-3 text-muted-foreground" />
                          <span>{member.phone}</span>
                        </div>
                      </div>
                      <div className="flex items-center gap-4">
                        <Badge variant="outline">{member.department}</Badge>
                        <div className="text-sm text-muted-foreground">
                          {member.activeProjects} projects, {member.activeTasks} tasks
                        </div>
                      </div>
                    </div>
                    <DropdownMenu>
                      <DropdownMenuTrigger asChild>
                        <Button variant="ghost" size="icon">
                          <MoreHorizontal className="h-4 w-4" />
                          <span className="sr-only">Actions</span>
                        </Button>
                      </DropdownMenuTrigger>
                      <DropdownMenuContent align="end">
                        <DropdownMenuItem>View Profile</DropdownMenuItem>
                        <DropdownMenuItem>Edit Member</DropdownMenuItem>
                        <DropdownMenuItem>Assign to Project</DropdownMenuItem>
                        <DropdownMenuItem>Remove</DropdownMenuItem>
                      </DropdownMenuContent>
                    </DropdownMenu>
                  </div>
                ))}
              </div>
            </CardContent>
          </Card>
        </TabsContent>
        <TabsContent value="departments" className="space-y-4">
          <Card>
            <CardContent className="p-6 text-center text-muted-foreground">
              Department view would show team members grouped by their departments.
            </CardContent>
          </Card>
        </TabsContent>
      </Tabs>
    </div>
  )
}
