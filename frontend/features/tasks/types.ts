export interface Assignee {
  name: string
  avatar: string
}

export interface Task {
  id: string | number
  title: string
  description: string
  project: string
  status: "In Progress" | "Not Started" | "Completed"
  dataInicio: string
  dataFim: string
  assignee: Assignee
  activityId?: number
}

export interface Activity {
  id: number
  name: string
  description: string
  tasks: Task[]
}
