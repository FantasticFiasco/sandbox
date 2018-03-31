# Run load test with the following command:
# locust --host=http://localhost:3000 -f ./nested_tasks.py

from locust import HttpLocust, TaskSet, task

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

class Behavior(TaskSet):
    tasks = {Status:10}   # 10x times compared to 'index'

    @task
    def index(self):
        self.client.get("/")

class User(HttpLocust):
    task_set = Behavior