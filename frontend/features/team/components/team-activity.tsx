import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar"
import { Button } from "@/components/ui/button"
import { Card, CardContent } from "@/components/ui/card"
import { CheckCircle2, Clock, MessageSquare, Plus, UserPlus } from "lucide-react"

export default function TeamActivity() {
  const activities = [
    {
      id: 1,
      user: {
        name: "Alex Morgan",
        avatar: "/placeholder.svg?height=32&width=32",
      },
      action: "completed",
      target: "Design homepage wireframes",
      project: "Website Redesign",
      time: "2 hours ago",
    }
  ]

  const getActionIcon = (action: string) => {
    switch (action) {
      case "completed":
        return <CheckCircle2 className="h-4 w-4 text-green-500" />
      case "commented":
        return <MessageSquare className="h-4 w-4 text-blue-500" />
      case "assigned":
        return <UserPlus className="h-4 w-4 text-purple-500" />
      case "updated":
        return <Clock className="h-4 w-4 text-amber-500" />
      case "created":
        return <Plus className="h-4 w-4 text-indigo-500" />
      default:
        return <Clock className="h-4 w-4 text-gray-500" />
    }
  }

  return (
    <Card>
      <CardContent className="p-0">
        <div className="flex items-center justify-between p-4 border-b">
          <h3 className="text-lg font-semibold">Team Activity</h3>
          <Button variant="outline" size="sm">
            View All
          </Button>
        </div>
        <div className="divide-y">
          {activities.map((activity) => (
            <div key={activity.id} className="flex items-start gap-4 p-4">
              <Avatar className="h-10 w-10">
                <AvatarFallback>{activity.user.name.charAt(0)}</AvatarFallback>
              </Avatar>
              <div className="flex-1 min-w-0">
                <div className="flex items-center gap-2">
                  <span className="font-medium">{activity.user.name}</span>
                  <span className="text-muted-foreground">{activity.action}</span>
                  <span className="font-medium">{activity.target}</span>
                </div>
                <div className="text-sm text-muted-foreground mt-1">in {activity.project}</div>
                <div className="flex items-center gap-1 text-xs text-muted-foreground mt-1">
                  {getActionIcon(activity.action)}
                  <span>{activity.time}</span>
                </div>
              </div>
            </div>
          ))}
        </div>
      </CardContent>
    </Card>
  )
}
