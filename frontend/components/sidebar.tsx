"use client"

import { cn } from "@/lib/utils"
import { BarChart3,BarChart2, Calendar, ChevronLeft, ClipboardList, Home, Menu, Settings, Users, DollarSign } from "lucide-react"
import Link from "next/link"
import { usePathname } from "next/navigation"
import { Button } from "@/components/ui/button"

interface SidebarProps {
  open: boolean
  setOpen: (open: boolean) => void
}

export default function Sidebar({ open, setOpen }: SidebarProps) {
  const pathname = usePathname()

  const routes = [
    {
      name: "Dashboard",
      path: "/",
      icon: Home,
    },
    {
      name: "Projects",
      path: "/projects",
      icon: ClipboardList,
    },
    {
      name: "Tasks",
      path: "/tasks",
      icon: BarChart3,
    },
    {
      name: "Team",
      path: "/team",
      icon: Users,
    },
    {
      name: "Calendar",
      path: "/calendar",
      icon: Calendar,
    },
    {
      name: "Settings",
      path: "/settings",
      icon: Settings,
    },
    {
      name: "Indicadores",
      path: "/indicadores",
      icon: BarChart2,
    },
    {
      name: "Rubricas",
      path: "/rubricas",
      icon: DollarSign,
    },
  ]

  return (
    <>
      <Button variant="ghost" size="icon" className="fixed top-4 left-4 z-50 md:hidden" onClick={() => setOpen(!open)}>
        <Menu className="h-5 w-5" />
      </Button>
      <div
        className={cn(
          "fixed inset-y-0 left-0 z-40 w-64 bg-background border-r transition-transform duration-300 ease-in-out",
          open ? "translate-x-0" : "-translate-x-full",
          "md:translate-x-0",
        )}
      >
        <div className="flex items-center justify-between h-16 px-4 border-b">
          <Link href="/" className="flex items-center gap-2 font-semibold text-xl">
            <ClipboardList className="h-6 w-6" />
            <span>ProjectHub</span>
          </Link>
          <Button variant="ghost" size="icon" className="md:flex hidden" onClick={() => setOpen(!open)}>
            <ChevronLeft className="h-5 w-5" />
          </Button>
        </div>
        <nav className="p-4 space-y-1">
          {routes.map((route) => {
            const isActive = pathname === route.path
            return (
              <Link
                key={route.path}
                href={route.path}
                className={cn(
                  "flex items-center gap-3 px-3 py-2 rounded-md text-sm font-medium transition-colors",
                  isActive ? "bg-primary text-primary-foreground" : "hover:bg-muted",
                )}
              >
                <route.icon className="h-5 w-5" />
                <span>{route.name}</span>
              </Link>
            )
          })}
        </nav>
      </div>
    </>
  )
}
