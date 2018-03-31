from locust import TaskSet, task

class Status(TaskSet):
    @task
    def login(self):
        self.client.post("/login", json={"username":"ellen_key", "password":"education"})

    @task
    def logout(self):
        self.client.post("/logout", json={"username":"ellen_key", "password":"education"})

    @task
    def stop(self):
        self.interrupt()